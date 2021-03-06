import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './login.component';
import { RequestCardComponent } from './request-card.component';
import { WelcomeComponent } from './welcome.component';
import { CardDetailComponent } from './card-detail.component';
import { FinalComponent } from './final.component';
import { AuthenticationService } from './authentication.service';
import { ReprintCardComponent } from './reprint.component';
import { ReprintFinalCardComponent } from './reprint-final.component';
import { RequestCardService } from "./request-card-service";
import { CommonService } from "./common.service";
import { EqualValidator } from './equal-validator.directive';
import { UserComponent } from './user.component';

import { ForgotPassword } from "./forgot-password.component";
@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            {
                path: '',
                component: LoginComponent
            },
            {
                path: 'welcome',
                component: WelcomeComponent
            },
            {
                path: 'request_card',
                component: RequestCardComponent

            },
            {
                path: 'user',
                component: UserComponent
            },
            {
                path: 'card_detail',
                component: CardDetailComponent
            },
            {
                path: 'final',
                component: FinalComponent
            },
            {
                path: 'reprint',
                component: ReprintCardComponent
            }
            ,
            {
                path: 'reprint_final',
                component: ReprintFinalCardComponent
            },

            {
                path: 'forgotPassword',
                component: ForgotPassword
            }
        ])],
    declarations: [
        AppComponent,
        LoginComponent,
        RequestCardComponent,
        WelcomeComponent,
        UserComponent,
        CardDetailComponent,
        FinalComponent,
        ReprintCardComponent,
        ReprintFinalCardComponent,
        ForgotPassword,
        EqualValidator
    ],
    providers: [
        AuthenticationService,
        RequestCardService,
        CommonService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
