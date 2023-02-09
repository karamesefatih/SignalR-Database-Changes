using Microsoft.AspNetCore.SignalR;
using SignalR_Backend.Data;
using SignalR_Backend.Models;
using System.Text.RegularExpressions;

namespace SignalR_Backend.Hubs
{
    public class PersonHub : Hub
    {
        private readonly BackendContext _context;
        public PersonHub(BackendContext context)
        {
            _context = context;
        }
        //Bu soket ile Veritabanına ekleme yapılacak ve 45 numaralı clientlara mesaj gidecek
        public async Task AddPersonByNameSurname(string surname, string name)
        {
            tb_name_surname tb_Name_Surname = new tb_name_surname();
            tb_Name_Surname.surname = surname;
            tb_Name_Surname.name = name;
            _context.Add(tb_Name_Surname);
            await _context.SaveChangesAsync();
            await Clients.Group("45").SendAsync("ReceiveMessageByGroup", "Ekleme Yapıldı");
        }
        public async Task GetNames()
        {
            var persons = _context.tb_name_surname.ToList();
            await Clients.All.SendAsync("ReceiveNames", persons);
        }
        public async Task SendNameByGroup(int id_home)

        {
            tb_name_surname tb_Places = new tb_name_surname();
            tb_Places.name = "karamesefatih";
            _context.SaveChanges();
            await Clients.Group("45").SendAsync("ReceiveMessageByGroup", "Güncelleme Yapıldı");


        }
        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);
        }
    }
}
