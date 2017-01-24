import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { RequestCardService } from "./request-card-service";
import { UserService } from "./user.service";
import { CardRequest } from './Request-Card';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from './common.service';

declare var componentHandler: any;

@Component({
    selector: 'card-detail',
    templateUrl: 'app/card-detail.component.html',
    providers: [
        RequestCardService, UserService
    ]
})
export class CardDetailComponent extends OnInit implements AfterViewInit {

    public expirationDate: string;
    public mailingAddress: string;
    private cardHolderName: string;
    private cardTemplateUrl: string;

    constructor(
        private requestCardService: RequestCardService,
        private userService: UserService,
        private router: Router,
        private commonService: CommonService) {

        super();

        if (this.commonService.getCardHolderName() != null) {
            this.cardHolderName = commonService.getCardHolderName();
        }
        if (this.commonService.getCardTemplateUrl() != null) {
            this.cardTemplateUrl = commonService.getCardTemplateUrl();
        }
    }

    ngOnInit() {
        this.GetUserDetails(this.commonService.getTokenUserId());
        this.CardExpireDateCalculate();
    }

    CardExpireDateCalculate() {
        var currentdate = new Date();
        var currentMonth = currentdate.getMonth();
        currentMonth = currentMonth + 1;
        this.expirationDate = currentdate.getFullYear()
            + 5 + "-" + currentMonth + "-"
            + currentdate.getDate();
    }

    GetUserDetails(Id) {
        let currentObject = this;
        this.userService.getUser(Id)
            .then(function (result) {
                currentObject.mailingAddress = result.maillingAddress;
            });
    }

    Submit(): void {
        this.requestCardService.CardRequest(this.commonService.getTokenUserId(), 1, this.cardHolderName, this.expirationDate)
            .subscribe(user => {
                this.router.navigate(['/final']);
            }, error => {
                alert("Card registration is failed");
            }
            );
    }

    ngAfterViewInit() {
        componentHandler.upgradeAllRegistered();
    }

    ngDoCheck() {
        //this.mailingAddress = localStorage.getItem('mailaddress');
    }
}

