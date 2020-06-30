<template>
  <div class="hello">
    <b-button @click="showModal = true">Add Note</b-button>
    <div class="container">
      <b-table :items="items" :fields="fields" outlined>
        <template v-slot:cell(show_details)="row">
          <b-button size="sm" @click="remove(row.item.id)" variant="danger" class="mr-2">
            <i class="fa fa-trash" aria-hidden="true"></i>
          </b-button>

          <b-button @click="edit(row.item)">Edit</b-button>
        </template>
      </b-table>
    </div>

    <div v-if="showModal">
      <transition name="modal">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title">Add Note</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true" @click="showModal = false, editc = false">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <p v-if="errors.length">
                    <b>Please correct these errors:</b>
                    <ul>
                      <li v-for="error in errors">{{ error }}</li>
                    </ul>
                  </p>
                  <form @submit="formSubmit">
                    <strong>Name:</strong>
                    <input id="name" type="text" class="form-control" v-model="form.firstName" />
                    <strong>Last Name:</strong>
                    <input id="name" class="form-control" v-model="form.lastName" />
                    <strong>Phone:</strong>
                    <input class="form-control" v-model="form.phone" />
                    <strong>Email:</strong>
                    <input id="email" class="form-control" v-model="form.email" />
                    <br />
                    <button class="btn btn-success">Submit</button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "Home",
  props: {
    msg: String
  },
  data() {
    return {
      showModal: false,
      editc: false,
      editIndex: -1,
      errors: [],
      form: {
        id: "",
        firstName: "",
        lastName: "",
        email: "",
        phone: ""
      },
      fields: ["firstName", "lastName", "phone", "email", "show_details"],
      items: []
    };
  },
  created() {
    this.getall();
  },
  methods: {
    getall() {
      axios.get("api/Notes/All").then(response => (this.items = response.data));
    },
    remove(id) {
      axios.delete("api/Note/Delete/" + id);
      this.getall();
    },
    edit(note) {
      this.editc = true;
      this.editIndex = note.id;
      this.form = note;
      this.showModal = true;
    },
    formSubmit(e) {
      e.preventDefault();

      if (
        this.form.firstName &&
        this.form.lastName &&
        this.form.phone &&
        this.form.email
      ) {
        if (!this.editc) {
          const formData = new FormData();
          formData.append("FirstName", this.form.firstName);
          formData.append("Phone", this.form.phone);
          formData.append("Email", this.form.email);
          formData.append("LastName", this.form.lastName);
          axios.post("api/Notes/Create", formData);
        }
        if (this.editc) {
          console.log(this.editc);
          const formData = new FormData();
          formData.append("id", this.form.id);
          formData.append("FirstName", this.form.firstName);
          formData.append("Phone", this.form.phone);
          formData.append("Email", this.form.email);
          formData.append("LastName", this.form.lastName);
          axios.put("api/Note/Edit", formData);
          this.editc = false;
        }
        this.showModal = false;
        this.getall();
      }

      this.errors = [];

      if (!this.form.phone){
        this.errors.push('You must specify the phone number!');
      }
      if (!this.form.email){
        this.errors.push('You must specify an email address!');
      }
      if (!this.form.firstName){
        this.errors.push('You must specify the Name!');
      }
      if (!this.form.lastName){
        this.errors.push('You must specify the Last Name!');
      }

    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
.container {
  padding: 5px;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
</style>
