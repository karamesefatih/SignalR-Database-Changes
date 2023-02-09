<template>
  <div>
    <div>karamesefatih</div>
    <div>
      <div>
        <span>name</span>
        <input type="text" v-model="name" />
      </div>
      <div>
        <span>surname</span>
        <input type="text" v-model="surname" />
      </div>
      <button @click="gonder">Send</button>
      <div>
        <table>
          <tr v-for="data in roomData" :key="data.id">
            <th>{{ data.name }}</th>
            <th>{{ data.surname }}</th>
          </tr>
        </table>
      </div>
      <hr> 
      <hr>

      <!-- <div>
        <table>
          <tr v-for="data in room2data" :key="data.id">
            <th>{{ data.name }}</th>
            <th>{{ data.id_home }}</th>
            <th>{{ data.icon }}</th>
          </tr>
        </table>
      </div> -->
    </div>
  </div>
</template>

<script>
import axios from "axios";
import * as signalR from "@microsoft/signalr";
import { looseIndexOf } from "@vue/shared";
export default {
  data() {
    return {
      connection: null,
      roomData: [],
      room2data:[],
      name:"",
      id_home:"",
      
    };
  },

  mounted() {
    this.getData();
    
  },

  methods: {
    async getData() {
      this.connection = new signalR.HubConnectionBuilder()
      //Web socketin URL'sini girdik
        .withUrl("https://localhost:7223/personHub")
        .build();

      this.connection
        .start()
        .then(() => {
          this.connection.invoke("GetNames");
          //Web Sokete bağlandığında GetNames ve AddToGroup methodunu çalıştıracak
          //GetNames isimleri ve soyisimleri getiriyor istediğimiz şekilde değiştirebiliriz mesela sadece isimi ahmet olanları almak istersek sokketin bir değer almasını sağlarız o kadar
          //AddToGroup Clientlerı gruplayıp sadece ilgili clientlara veri gelmesini sağlıyor
          //AddToGroup string bir değer allıyor ve şuan bu string değer teamName'a eşit istersek frontendde de bunu ayarlayabiliriz

          this.connection.invoke("AddToGroup","45")
        })
        .catch((err) => console.log("connecting hub failed err is : ", err));


         //ReceiveNames subscribes to the socket we are running on line 80
         //We listen to all people. We can shape it as we want by sending values to the hub.
        this.connection.on("ReceiveNames", (data) => {
        console.log(data);
        this.roomData = data;
      });
    
      this.connection.on("GetNames")
      this.connection.on("AddToGroup", "45");
      // With ReceiveMessageByGroup, it listens to the AddPersonByNameSurname method on line 100 and prints the Update done message to the console
      this.connection.on("ReceiveMessageByGroup", (data) => {

        console.log(data);
      });
    },

    gonder() {

      console.log('this.surname,this.name :>> ', this.surname,this.name);
      
      this.connection.invoke("AddPersonByNameSurname",this.surname,this.name).catch((err) => {
        console.log(err);
      });

     // this.connection.invoke("SendNameByGroup", 45).catch((err) => {
    //    console.log(err);
      //});
// Refreshing the data when the button is clicked
      this.connection.invoke("GetNames");
    },
  },
};
</script>

<style scoped></style>
