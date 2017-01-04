import { Component, Directive, AfterViewInit } from '@angular/core';

declare var componentHandler: any;

@Component({
    selector: 'reprint-final',
    templateUrl: 'app/reprint-final.component.html',

})

export class ReprintFinalCardComponent implements AfterViewInit {
    ngAfterViewInit() {
        componentHandler.upgradeAllRegistered();
    }
}