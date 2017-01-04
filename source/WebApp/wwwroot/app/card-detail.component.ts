import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpRequestCardService } from "./request-card-service";
import { userService } from "./user.service";
import { CardRequest } from './Request-Card';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from './common.service';

declare var componentHandler: any;

@Component({
    selector: 'card-detail',
    templateUrl: 'app/card-detail.component.html',
    providers: [
        HttpRequestCardService, userService
    ]
})
export class CardDetailComponent extends OnInit implements AfterViewInit {

    expirationDate: string;
    mailingAddress: string;

    public address: string = "non Assigned";
    mode = 'Observable';
    errorMessage: string;
    cardDeatails: CardRequest[];
    _cardDeatails: CardRequest;
    private cardHolderName: string;
    private cardTemplateUrl: string;

    constructor(
        private _service: HttpRequestCardService,
        private _userService: userService,
        private router: Router,
        private commonService: CommonService) {

        super();

        this.cardHolderName = commonService.getCardHolderName();
        this.cardTemplateUrl = commonService.getCardTemplateUrl();

    }

    GetRequestCardDetails() {
        this._service.getRequestCardDetails()
            .subscribe(cardDetails => this.cardDeatails = cardDetails, error => this.errorMessage = <any>error);
        alert(this.cardDeatails);
        alert(this.errorMessage);

    }

    GetRequestCardDetail(Id) {
        this._service.getACard(Id)
            .then(function (result) {
                var json = result.Data;

            });

    }

    CardRequest() {
        this._service.CardRequest(1, 1, 1, "Nishan", this.expirationDate);
    }
    CardExpireDateCalculate() {
        var currentdate = new Date();
        var currentMonth = currentdate.getMonth();
        currentMonth = currentMonth + 1;
        this.expirationDate = currentdate.getFullYear() + 5 + "-" + currentMonth + "-" + currentdate.getDate();
    }
    GetUserDetails(Id) {
        var mailaddress;
        this._userService.getAUser(Id)
            .then(function (result) {
                localStorage.setItem('mailaddress', result.Data.MailingAddress);
            });
    }

    Submit(): void {
        //var _this = this;
        ////this.CardRequest();
        /*localStorage.setItem('CardDetailsCompleted',"true");
        alert("Make Request");
        _this.router.navigate(['/final']); */
        alert("card Holder Name:" + this.cardHolderName);
    }
    ngOnInit() {
        this.GetRequestCardDetail(1);
        this.GetUserDetails(1);
        this.CardExpireDateCalculate();
    }
    ngAfterViewInit() {
        componentHandler.upgradeAllRegistered();

    }
    ngDoCheck() {
        this.mailingAddress = localStorage.getItem('mailaddress');
    }
}

