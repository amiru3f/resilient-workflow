namespace TemporalTestSolution.EmailWorker;

public record EmailConfiguration(string From = "", string To = "", string Username = "", string Password = "", string Host = "smtp.gmail.com", int Port = 587);