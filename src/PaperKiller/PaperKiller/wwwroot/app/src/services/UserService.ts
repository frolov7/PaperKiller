// services/UserService.ts
import axios from "axios";
import { AuthorizationDTO } from "../interfaces/AuthorizationDTO";
import { RegistrationDTO } from "../interfaces/RegistrationDTO";
import { StudentDTO } from "../interfaces/StudentDTO";

class UserService {
	static API_URL = "/api/v1/"; // Относительный путь

	static async authenticateUser(authorizationData: AuthorizationDTO): Promise<any> {
		try {
			const response = await axios.post(`${this.API_URL}authorization`, authorizationData);
			if (response.status === 200) {
				if (authorizationData.login == "Admin" && (authorizationData.password === "Manager" || authorizationData.password === "Commandant")) {
					return authorizationData.password;
				}
				else {
					const studentData: StudentDTO = response.data;
					localStorage.setItem("userID", JSON.stringify(studentData.id));
					return studentData;
				}
			}
		} catch (error) {
			throw new Error("Authentication failed");
		}
	}

	static async registerUser(registrationData: RegistrationDTO) {
		try {
			const response = await axios.post(`${this.API_URL}register`, registrationData);
			return response.data;
		} catch (error) {
			console.error("Error registering user:", error);
			throw error;
		}
	}
}

export default UserService;
