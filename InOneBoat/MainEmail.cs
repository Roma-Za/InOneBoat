using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace InOneBoat
{
    public class MainEmail
    {
        public string ConnString { set; get; }
        public int ID { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Smtp { set; get; }
        public MainEmail(string cs)
        {
            ConnString = cs;
            loadEmail();
            Smtp = "smtp.yandex.ru";
        }

        private void loadEmail()
        {
            string commandCu = "SELECT * FROM main_email";
            using (SqlConnection sql_connect = new SqlConnection(ConnString))
            {

                sql_connect.Open();
                SqlCommand cmd = sql_connect.CreateCommand();
                cmd.CommandText = commandCu;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    #region присвоение
                    int Id = 0;
                    String em = "";
                    String pass = "";
                    try
                    {
                        Id = rdr.GetInt32(0);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ошибка конвертации " + Id);
                    }

                    try
                    {
                        em = rdr.GetString(1);
                    }
                    catch (Exception)
                    {

                        em = "";
                    }

                    try
                    {
                        pass = rdr.GetString(2);
                    }
                    catch
                    {
                        pass = "";
                    }

                    #endregion

                    ID = Id;
                    Email = em;
                    Password = pass;
                }
            }
        }

        public void EditEmail(string e, string p)
        {
            if (!e.Equals(Email))
            {
                string comm = "UPDATE main_email SET email = @em";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm;
                    cmd.Parameters.AddWithValue("@em", e);
                    cmd.ExecuteNonQuery();
                }
                Email = e;
            }
            if (!p.Equals(Password))
            {
                string comm2 = "UPDATE main_email SET password = @pass";
                using (SqlConnection sql_connect = new SqlConnection(ConnString))
                {
                    sql_connect.Open();
                    SqlCommand cmd = sql_connect.CreateCommand();
                    cmd.CommandText = comm2;
                    cmd.Parameters.AddWithValue("@pass", p);
                    cmd.ExecuteNonQuery();
                }
                Password = p;
            }
        }

        /// <summary>
        /// Отправка письма на почтовый ящик C# mail send
        /// </summary>
        /// <param name="smtpServer">Имя SMTP-сервера</param>
        /// <param name="from">Адрес отправителя</param>
        /// <param name="password">пароль к почтовому ящику отправителя</param>
        /// <param name="mailto">Адрес получателя</param>
        /// <param name="caption">Тема письма</param>
        /// <param name="message">Сообщение</param>
        /// <param name="attachFile">Присоединенный файл</param>
        public void SendMail(string[] mailto, string caption, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Email);
                foreach (var item in mailto)
                {
                    mail.To.Add(new MailAddress(item));
                }

                mail.Subject = caption;
                mail.Body = message;

                SmtpClient client = new SmtpClient();
                client.Host = Smtp;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(Email.Split('@')[0], Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

    }
}
