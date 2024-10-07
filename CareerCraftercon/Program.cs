//using System.Net;

//namespace CareerCraftercon
//{
//    public class EmployerDTO
//    {
//        public int EmployerId { get; set; }
//    }

//    internal class Program
//    {
//        static async Task Main(string[] args)
//        {
//            string BaseURL = "https://localhost:7134/";
//            string SpecificURL = "api/Employer/employers";
//            string ActualURL = BaseURL + SpecificURL + "/2"; 

//            WebClient aClientObject = new WebClient();
//            aClientObject.Headers["Content-Type"] = "application/json";

//            string response = aClientObject.DownloadString(ActualURL); 
//            Console.WriteLine("Response from API:\n");
//            Console.WriteLine(response);
//            Console.ReadKey();
//        }
//    }
//}
