import { StudentDTO } from "./StudentDTO";

export interface StudentDTOAdd extends StudentDTO {
    login: string;
    password: string;
    userType: string;
}