import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { CommonService } from './common.service';
import { Router } from '@angular/router';

@Component({
    selector: 'edit-profile',
    templateUrl: 'app/user.component.html',
    providers: [
        UserService
    ]
})
export class UserComponent {

    constructor(
        private _service: UserService,
        private router: Router
    ) {
    }

    userDetails(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch, file) {
       let test = null;
        this._service.UserData(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch, file)
        .subscribe(user=>{            
            this.router.navigate(['/welcome']);
        },error=>{
            alert("User registration is failed");
        });
        

    }
}



