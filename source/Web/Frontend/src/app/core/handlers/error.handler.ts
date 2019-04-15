import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";
import { AppModalService } from "../services/modal.service";

@Injectable()
export class AppErrorHandler implements ErrorHandler {
    constructor(private readonly injector: Injector) { }

    handleError(error: any) {
        if (error instanceof HttpErrorResponse) {
            switch (error.status) {
                case 401: {
                    const router = this.injector.get<Router>(Router);
                    router.navigate(["/login"]);
                    return;
                }
                case 422: {
                    const appModalService = this.injector.get<AppModalService>(AppModalService);
                    appModalService.alert(error.error);
                    return;
                }
                case 400: {
                    const appModalService = this.injector.get<AppModalService>(AppModalService);
                    let hata = "<ul>";
                    for (const hata2 in error.error) {
                        hata += "<li> " + error.error[hata2] + "</li>";
                    }
                    hata += "</ul>";
                    appModalService.alert(hata);
                    return;
                }
            }
        }

        console.error(error);
    }
}
