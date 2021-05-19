import '../styles/site.css';

import 'alpinejs';
import axious from 'axios';

import { library, dom } from "@fortawesome/fontawesome-svg-core";
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import axios from 'axios';

library.add(fas, far, fab);
dom.watch();

interface User{//cal: This just helps with dot notation. Doesent necessarily need every field.
    firstName: string,
    lastName: string,
    id: number
}

export function setupNav() {
    return {
        toggleMenu() {
            var headerNav = document.getElementById('headerNav');
            if (headerNav) {
                if (headerNav.classList.contains('hidden')) {
                    headerNav.classList.remove('hidden');
                } else {
                    headerNav.classList.add('hidden');
                }
            }
        }
    }
}

//cal: This stuff got changed around when converting to Alpine.js. Remeber that inorder to use these things we put them inside a x-data attribute in the html.
//cal: we also imported axios into out package.json.
//cal: javascript is camel case while c# is pascal case.
//cal: we change the code inside the views because we are no longer using server side code.
export function setupUsers() {
    return {
        users: [] as User[],
        async mounted()//cal: this is where we make a call to the Api. Also be aware that everything in js is asynchronous. Also the name mounted matches what we have in html. But could be whatever we wanted, e.g loadUsers().
        {            
            await this.loadUsers();
        },

        async deleteUser(currentUser: User)
        {
            if(confirm(`Are you sure you wish to delete ${currentUser.firstName} ${currentUser.lastName}?`))
            {
                await axios.delete(`https://localhost:5101/api/users/${currentUser.id}`);//cal: keep in mind that this isnt a single quote. Its from the tilde key.
                await this.loadUsers();
            }
        },

        async loadUsers()//cal: the reason we moved this into loadUsers() was so it can be called like a page refresh in other functions
        {
            try{
                const response = await axios.get("https://localhost:5101/api/users");
                this.users = response.data;
            }catch(error){
                console.log(error);
            }
        }
    }
}//end setupUsers

export function createOrUpdateUser(){
    return{
        user: {} as User,
        async create()
        {
            try{
                //cal: this part isnt for our assingment. But when working with a date you may need to convert the object by wrapping it as a this.user.date = new Date(this.user.date);
                await axios.post("https://localhost:5101/api/users", this.user);
                window.location.href="/users";//cal: navigates back to users page.
            }catch(error){
                console.log(error);
            }
        }
    }
}//end createOrUpdateUser