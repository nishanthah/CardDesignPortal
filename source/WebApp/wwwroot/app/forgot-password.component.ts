import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {ResetPasswordService} from './reset-password-service';

@Component({
    selector: 'forgotPassword',
    templateUrl: 'app/forgot-password.component.html',
    providers: [
        ResetPasswordService
    ]
})

export class ForgotPassword {

     public error="";
     constructor(private resetPasswordService:ResetPasswordService,private router: Router) {
         
    }
    
    sendEmail(email)
    {
        var currentObject=this;
        this.resetPasswordService.sendResetCode(email)
            .then(function (result) {
                if(result.Data.userExist)
                {
                    currentObject.error="";
                    alert("Reset code has sent to your email");
                }
                else
                   { 
                       currentObject.error="User doesn't exist...!";
                   }
            });
     
    }
    resetPassword(email,resetcode,password)
    {
        var currentObject=this;
         this.resetPasswordService.ResetPassword(email,parseInt(resetcode),password)
            .then(function (result) {
                 if(result.Data.userExist)
                 {
                     currentObject.error="";
                     if (result.Data.result) {
                         
                         currentObject.router.navigate(['/']);
                     }
                     else
                         currentObject.error="Invalid reset code...!";
                 }
                 else
                 {
                    currentObject.error="User doesn't exist...!";
                 }
            });
    }
}