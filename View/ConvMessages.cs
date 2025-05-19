namespace PolMedUMG.View
{
    public class ConvMessages
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public ConvMessages(string sender, string receiver, DateTime date, string content)
        {
            Sender = sender;
            Receiver = receiver;
            Date = date;
            Content = content;
        } 
        
    }
    public class MessageRepository
    {
        public List<ConvMessages> GetMessages(string sender, string receiver) // ta metoda zwraca wszystkie wiadomosci
        {

            var ConvMessages = new List<ConvMessages>
            {
                new ConvMessages("dr. Witt", "patient", new DateTime(2004, 12, 12), "Witam ty kurwo jebana"),
                new ConvMessages("patient", "dr. Witt", DateTime.Now, "Cześć, jak się masz?"),
                new ConvMessages("dr. Witt", "Patient", DateTime.Now, "jebać cie kasztanie"),
                new ConvMessages("dr. Marek Towarek", "patient", DateTime.Now, "tescik1"),
                new ConvMessages("patient", "dr. Tomasz Kowalski", DateTime.Now, "tescik2"),
                new ConvMessages("patient", "dr. Andrzej Pędrak", DateTime.Now, "tescik3333"),
                new ConvMessages("dr. Witt", "patient", DateTime.Now, "w sumie dziala"),
                new ConvMessages("patient", "dr. Witt", DateTime.Now, "a no")
            };
            return ConvMessages;
        }
        public List<ConvMessages> GetMessagesFrom(string doctorName, string patientName) // ta metoda zwraca wiadomosci miedzy dwoma uzytkownikami
        {
            var ConvMessages = new List<ConvMessages>
            {
                new ConvMessages("dr. Witt", "patient", new DateTime(2004, 12, 12), "Witam serdecznie kochaniutki"),
                new ConvMessages("patient", "dr. Witt", DateTime.Now, "Cześć, jak się masz?"),
                new ConvMessages("dr. Witt", "Patient", DateTime.Now, "Nie twoj interes kasztanie"),
                new ConvMessages("dr. Marek Towarek", "patient", DateTime.Now, "tescik1"),
                new ConvMessages("patient", "dr. Tomasz Kowalski", DateTime.Now, "tescik2"),
                new ConvMessages("patient", "dr. Andrzej Pędrak", DateTime.Now, "tescik3333"),
                new ConvMessages("dr. Witt", "patient", DateTime.Now, "w sumie dziala"),
                new ConvMessages("patient", "dr. Witt", DateTime.Now, "a no"),
                new ConvMessages("patient", "dr. Eryk Fryderyk", DateTime.Now, "a no")
            };
            var filteredMessages = new List<ConvMessages>();

            foreach (var msg in ConvMessages)
            {
                if ((msg.Sender == patientName && msg.Receiver == doctorName) ||
                    (msg.Sender == doctorName && msg.Receiver == patientName))
                {
                    filteredMessages.Add(msg);
                }
            }

            return filteredMessages;
        }
    }
}
