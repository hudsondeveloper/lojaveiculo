using System.Net;
using System.Net.Mail;

namespace ConsoleRabbit
{

    public static class Email
    {
        public static void SendMessage(string email)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("pwmaniapw@gmail.com");
                    mail.To.Add(new MailAddress(email, "RECEBEDOR"));
                    mail.Subject = "Veiculo cadastrado";
                    mail.Body = "Veiculo cadastrado com sucesso";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("pwmaniapw@gmail.com", "pwpwpwpw");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                Console.WriteLine("E-mail enviado");

            }
            catch (System.Exception erro)
            {
                Console.WriteLine("Deu erro");
                //trata erro
            }
            finally
            {
            }

        }
    }
}