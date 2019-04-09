import { Component } from "@angular/core";
import { User } from "./user.model";
import { UserService } from "./user.service";

@Component({ selector: "app-rxjs", templateUrl: "./rxjs.component.html" })
export class AppRxjsComponent {
    users: User[] | undefined;

    constructor(private readonly userService: UserService) {
        this.userService.getUsersCitiesCompanies().subscribe((users: User[]) => { this.users = users; });
    }
}
