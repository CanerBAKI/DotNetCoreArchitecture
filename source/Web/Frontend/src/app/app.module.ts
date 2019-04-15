import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { routes } from "./app.routes";
import { AppCoreModule } from "./core/core.module";
import { LoaderComponent } from './components/loader/loader.component';
import { Globals } from "./services/globals";

@NgModule({
    bootstrap: [AppComponent],
    declarations: [AppComponent, LoaderComponent],
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),
        AppCoreModule
    ],
    providers:[Globals]
})
export class AppModule { }
