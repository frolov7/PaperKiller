export interface StudentDTO {
    id: number;
    login: string;
    password: string;
    userType: string;
    name: string;
    surname: string;
    phoneNumber: string;
    checkInDate: string;
    studak: string;
    gender: string;
    roomNumber: string;
    linenId: number | null;
    itemsId: number | null;
}