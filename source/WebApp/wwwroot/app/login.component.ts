import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { Router } from '@angular/router';
import { CommonService } from './common.service';
import { UserService } from './user.service';

@Component({
    selector: 'login-page',
    templateUrl: 'app/login.component.html',
    providers: [
        UserService
    ]
})
export class LoginComponent implements OnInit {

    public error = "";
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService,
        private commonService: CommonService,
        private userService: UserService
    ) { }

    ngOnInit() {
        this.authenticationService.logout();
    }

    login(username, password) {
        var currentObj = this;
        this.authenticationService.login(username, password)
            .then(function (result) {
                if (result != null) {
                    currentObj.commonService.setToken(result.access_token);

                    currentObj.userService.isUserRegister(currentObj.commonService.getTokenUserId())
                        .then(function (result) {
                            if (!result) {
                                currentObj.router.navigate(['/user']);
                            }
                            else {
                                currentObj.router.navigate(['/welcome']);
                            }
                        })
                }
                else if (username == null || username == "") {
                    currentObj.error = 'You must enter username.';
                }
                else if (password == null || password == "") {
                    currentObj.error = 'You must enter password.';
                }
                else {
                    currentObj.error = 'Username or password is incorrect';
                }
            });
    }
}



