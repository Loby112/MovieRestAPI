using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using MovieLib;

namespace ConcurrentTCPServer {
    internal class Program {
        private static string url = "https://movierestapi20220605202430.azurewebsites.net/api/Movies";
        private static int nextId = 1;
        private static List<Movie> moviesList = new List<Movie>();
            static void Main(string[] args) {
                Console.WriteLine("Server:");
                //Tells the operating system that all TCP connections on port 7 should be sent to this application
                TcpListener listener = new TcpListener(System.Net.IPAddress.Loopback, 43214);
                //Starts listening
                listener.Start();

                //Infinite loop to be able to handle more than one client
                while (true) {
                    //Blocks the thread until a client connects
                    TcpClient socket = listener.AcceptTcpClient();


                    //Starts a new thread with the incoming client, so that the application can handle several clients at the same time
                    Task.Run(() => {
                        HandleClient(socket);
                    });
                }
            }
        

            public static void HandleClient(TcpClient socket) {
                //Gets the stream (bi-directional) that connects the server and the client
                NetworkStream ns = socket.GetStream();
                //Creates a reader for easy access to what the client sends
                StreamReader reader = new StreamReader(ns);
                //Creates a writer for easily writing to the client
                StreamWriter writer = new StreamWriter(ns);

                APIMovies();
            //Reads all data until the client sends a newline (\r\n) and stores it in a string
                string message = reader.ReadLine();
                Console.WriteLine("Client wrote: " + message);
                //Lytter og tjekker på hvilken metode der bliver sendt
                if (message == "GetAllMovies"){
                    string movies = GetAllMovies();
                    writer.WriteLine(movies);
                }
                else if(message.Contains("GetByCountry")){
                    string moviesByCountry = GetByCountry(message);
                    writer.WriteLine(moviesByCountry);
                }
                //writes the same data back to the client and ends with newline (\r\n)
                else{
                    writer.WriteLine("Type GetAllMovies or GetByCountry + country");
                }
                //makes sure that the server sends the data immediately (it should wait for potentially more data)
                writer.Flush();
                //closes the connection, single use server.
                socket.Close();
            }

            public static string GetAllMovies(){
                string serializedData = JsonSerializer.Serialize(moviesList);
                return serializedData;
            }

            public static string GetByCountry(string country){
                //Splitter ordene så at jeg kan finde hvilket land man ønsker at få filmene fra
                string[] words = country.Split(" ");
                var result = moviesList.Find(m => m.Country == words[1]);
                string serializedData = JsonSerializer.Serialize(result);
                return serializedData;
            }
            
            public static async void APIMovies(){
                //Bruger http client til at indhente listen fra min API
                using (HttpClient client = new HttpClient()) {
                    var result = await client.GetAsync(url);
                    moviesList = await result.Content.ReadFromJsonAsync<List<Movie>>();
                }
        }

    }
}
