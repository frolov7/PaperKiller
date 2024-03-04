// services/StudentService.ts
import axios from "axios";
import { StudentChangeDataDTO } from "../interfaces/StudentChangeDataDTO";
import { StudentDTO } from "../interfaces/StudentDTO";
import { MyItemsDTO } from "../interfaces/MyItemsDTO";

class StudentService {
	static API_URL = "/api/v1/";

	static updateStudentData(userId: string, studentData: StudentChangeDataDTO) {
		return axios.put(`${this.API_URL}${userId}`, studentData)
			.then(response => response.data);
	}

	static async getStudentData(userId: string): Promise<StudentDTO> {
		try {
			const response = await axios.get<StudentDTO>(`${this.API_URL}showmydata/${userId}`);
			if (response.status === 200) {
				return response.data;
			} else {
				throw new Error(`Request failed with status ${response.status}`);
			}
		} catch (error) {
			console.error("Error fetching student data:", error);
			throw error;
		}
	}

	static async moveOut(userId: string): Promise<void> {
		try {
			const response = await axios.delete(`${this.API_URL}moveout/${userId}`);
			if (response.status === 200) {
				console.log("Student moved out successfully");
			} else {
				throw new Error(`Request failed with status ${response.status}`);
			}
		} catch (error) {
			console.error("Error moving out student:", error);
			throw error;
		}
	}

	static async showMyItems(userId: string): Promise<MyItemsDTO[]> {
		try {
			const response = await axios.get<MyItemsDTO[]>(`${this.API_URL}myitems/${userId}`);
			if (response.status === 200) {
				return response.data;
			} else {
				throw new Error(`Request failed with status ${response.status}`);
			}
		} catch (error) {
			console.error("Error fetching items:", error);
			throw error;
		}
	}
}


export default StudentService;
