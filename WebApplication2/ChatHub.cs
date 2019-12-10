using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebApplication2.Models;

namespace WebApplication2
{
    //Nếu sử dụng attribute Hubname thì khi gọi sẽ là "ChatHub" thay vì "chatHub"
    //[HubName("ChatHub")]
    public class ChatHub : Hub
    {
        //Defind Complex object instead of many parameter
        public class UserMessage
        {
            public string idsend { get; set; }
            public string message { get; set; }
            public string connectionID { get; set; }
            public string idconversatio { get; set; }
        }

        public class MessageHistory
        {
            public string message { get; set; }
            public string name { get; set; }
            public string date { get; set; }
        }

        private PasGoEntities2 db = new PasGoEntities2();
        //Hub does not support session, pass query string here
        public override async Task OnConnected()
        {
            System.Diagnostics.Trace.WriteLine(Context.ConnectionId + " - Connected / " + HttpContext.Current.User.Identity.Name + " - from - " + Context.QueryString["side"]);
            await base.OnConnected();
        }
        
        public override async Task OnDisconnected(bool stopCalled)
        {
            db.ModifyConnectionId(0, Context.ConnectionId, 0);
            System.Diagnostics.Trace.WriteLine(Context.ConnectionId + " - Disconnected / " + Context.User.Identity.Name);
            await base.OnDisconnected(stopCalled);
        }

        //Handle send message event from Staff side 
        public void StaffSend(string namesend, string message, string connectionID, string idconversation)
        {
            var idconn = Convert.ToInt32(idconversation);
            var status = db.Conversations.Where(x => x.IdConversation == idconn).FirstOrDefault().Status;
            if (status == true)
            {
                db.NewMessage(idconn, Convert.ToInt32(connectionID), 1);
                db.SaveMessage(idconn, false, message);
                var allconn = db.getAllConn(Convert.ToInt32(connectionID)).ToList();
                foreach (var item in allconn)
                {
                    //JS from Satff side is different from User side / update code later
                    Clients.Client(item.Conn).addNewMessageToPage(namesend, message);
                }
                Clients.Client(Context.ConnectionId).addNotificationToPage("Đã gửi");
            }else
                Clients.Client(Context.ConnectionId).addNotificationToPage("4");
        }
        //Handle send message event from User side 
        public void UserSend(string namesend, string message, string connectionID, string idconversation)
        {
            var idconn = Convert.ToInt32(idconversation);
            var status = db.Conversations.Where(x => x.IdConversation == idconn).FirstOrDefault().Status;
            if (status == true)
            {
                db.NewMessage(idconn, Convert.ToInt32(connectionID), 1);
                db.SaveMessage(idconn, true, message);
                var allconn = db.getAllConn(Convert.ToInt32(connectionID)).ToList();
                foreach (var item in allconn)
                {
                    Clients.Client(item.Conn).addNewMessageToPage(namesend, message);
                }
                Clients.Client(Context.ConnectionId).addNotificationToPage("Đã gửi");
            }else
                Clients.Client(Context.ConnectionId).addNotificationToPage("4");
        }

        //Handle send message event from User side, with complex object
        /*
        public void UserSend(string idsend, string message, string connectionID, string idconversation)
        {
            db.SaveMessage(Convert.ToInt32(idconversation), true, message);
            var allconn = db.getAllConn(Convert.ToInt32(connectionID)).ToList();
            foreach (var item in allconn)
            {
                Clients.Client(item.Conn).addNewMessageToPage(new UserMessage { idsend = idsend, message = message, connectionID = connectionID, idconversatio = idconversation });
            }
        }
        */

        //Thay cho OnConnected do không pass được parameter 
        public void SaveConnection(string id)
        {
            var result = db.ModifyConnectionId(Convert.ToInt32(id), Context.ConnectionId, 1);
        }
        //Chat history return parameter
        public void ChatHistory(string conversationid, string connectionID)
        {
            var history = db.ChatHistory(Convert.ToInt32(conversationid));
            foreach(var item in history.ToList())
            {
                Clients.Client(connectionID).addNewMessageToPage(item.FullName, item.Message, item.DateSent.ToString());
            }
        }

        //Chat history return object
        public IEnumerable<MessageHistory> chatHistoryObject(string conversationid)
        {
            var history = db.ChatHistory(Convert.ToInt32(conversationid));
            List<MessageHistory> result = new List<MessageHistory>();
            foreach(var item in history)
            {
                MessageHistory x = new MessageHistory { message = item.Message, name =  item.FullName, date = item.DateSent.ToString() };
                result.Add(x);
            }
            return result;
        }

        public void Notification(string idconn, string idreceived, string type)
        {
            var idre = Convert.ToInt32(idreceived);
            var idconversation = Convert.ToInt32(idconn);
            //Dùng số thì được convert thì không được????
            //idconn ở đây là connection chứ ko phải idstaff
            var status = db.Conversations.Where(x => x.IdConversation == idconversation).FirstOrDefault().Status;
            if (status == true)
            {
                if (type == "2")
                {
                    db.NewMessage(idconversation, idre, 0);
                }
                var allconn = db.getAllConn(idre).ToList();
                foreach (var item in allconn)
                    Clients.Client(item.Conn).addNotificationToPage(type);
            }
            else
            {
                var allconn = db.getAllConn(idre).ToList();
                foreach (var item in allconn)   
                    Clients.Client(item.Conn).addNotificationToPage("4");    
            }
        }

        public IEnumerable<GetConversationList_Result> ListConversation(string idrequest, string type)
        {
            List<GetConversationList_Result> result = db.GetConversationList(Convert.ToInt32(idrequest), Convert.ToInt32(type)).ToList();
            var x = result.Count();
                return result;
        }
    }

   
}