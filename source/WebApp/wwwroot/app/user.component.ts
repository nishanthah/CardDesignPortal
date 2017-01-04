import { Component, OnInit } from '@angular/core';
import {userService} from './user.service';

@Component({
    selector: 'edit-profile',
    templateUrl: 'app/profile.component.html',
})
export class UserComponent {
    constructor(private _service: userService) {
    }


    
    userDetails(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch, file) {
        this._service.UserData(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch, file);
    }
}



