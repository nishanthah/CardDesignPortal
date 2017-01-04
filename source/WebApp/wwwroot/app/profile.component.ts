import { Component, OnInit } from '@angular/core';


@Component({
    selector: 'edit-profile',
    templateUrl: 'app/profile.component.html'
})

export class ProfileComponent {


    // public firstName:string;
    //  public lastName:string;
    //public  phoneCode:string;
    //public  phoneNumber:string;
    //public  email:string;
    //public  address:string;
    //public  town:string;
    //public  kidsCif:string;
    //public  birthdaty:string;
    //public  accountBranch:string;
    //public  proPic:string;

    GetProfileDetails(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch) {
        alert(fName);


        /* this.firstName=fName;
         this.lastName=lName;
         this.phoneCode=code;
         this.phoneNumber=phoneNum;
         this.email=emailAddress;
         this.address=address;
         this.town=town;
         this.kidsCif=cif;
         this.birthdaty=dob;
         this.accountBranch=accBranch;
         */
    }

}



