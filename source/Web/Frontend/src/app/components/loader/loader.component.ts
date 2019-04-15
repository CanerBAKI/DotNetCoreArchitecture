import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoaderService } from '../../services/loader.service'
import { LoaderState } from './loader.model';

@Component({
    selector: 'app-loader',
    templateUrl: './loader.component.html',
    styleUrls: ['./loader.component.css']
})
export class LoaderComponent implements OnInit, OnDestroy {
    show = false;
    private _subscription: Subscription;
        
    constructor(private loaderService: LoaderService) {
    }

    ngOnInit() {
        this._subscription = this.loaderService.loaderState
            .subscribe((state: LoaderState) => {
                this.show = state.show;
            });
    }

    ngOnDestroy() {
        this._subscription.unsubscribe();
    }
}
