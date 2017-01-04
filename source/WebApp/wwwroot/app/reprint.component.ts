import { Component, Directive, AfterViewInit } from '@angular/core';

declare var componentHandler: any;

@Component({
    selector: 'reprint-card',
    templateUrl: 'app/reprint.component.html',

})

export class ReprintCardComponent implements AfterViewInit {
    ngAfterViewInit() {
        componentHandler.upgradeAllRegistered();
    }
}