import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppTokenService } from "../core/services/token.service";
import { AddUserModel } from "../models/add.user.model";
import { SignInModel } from "../models/signIn.model";
import { TokenModel } from "../models/token.model";
import { UpdateUserModel } from "../models/update.user.model";
import { UserModel } from "../models/user.model";
import { Globals } from "./globals";

@Injectable({ providedIn: "root" })
export class AppUserService {
    constructor(
        private readonly http: HttpClient,
        private readonly router: Router,
        private readonly appTokenService: AppTokenService,
        private readonly globals: Globals) { }

    add(addUserModel: AddUserModel) {
        return this.http.post<number>(this.globals.baseApiUrl+`Users`, addUserModel);
    }

    delete(userId: number) {
        return this.http.delete(this.globals.baseApiUrl +`Users/${userId}`);
    }

    list() {
        return this.http.get<UserModel[]>(this.globals.baseApiUrl +`Users`);
    }

    select(userId: number) {
        return this.http.get<UserModel>(this.globals.baseApiUrl +`Users/${userId}`);
    }

    signIn(signInModel: SignInModel): void {
        this.http
            .post<TokenModel>(this.globals.baseApiUrl +`Users/SignIn`, signInModel)
            .subscribe((tokenModel) => {
                if (tokenModel && tokenModel.token) {
                    this.appTokenService.set(tokenModel.token);
                    this.router.navigate(["/main/home"]);
                }
            });
    }

    signOut() {
        if (this.appTokenService.any()) {
            this.http.post(this.globals.baseApiUrl +`Users/SignOut`, {}).subscribe();
        }

        this.appTokenService.clear();
        this.router.navigate(["/login"]);
    }

    update(updateUserModel: UpdateUserModel) {
        return this.http.put(this.globals.baseApiUrl +`Users/${updateUserModel.userId}`, updateUserModel);
    }
}
