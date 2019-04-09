import { UserCity } from "./user.city.model";
import { UserCompany } from "./user.company";

export class User {
    userId: number | undefined;
    name: string | undefined;
    cities?: UserCity[];
    companies?: UserCompany[];
}
