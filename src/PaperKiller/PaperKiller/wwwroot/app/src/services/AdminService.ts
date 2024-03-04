import axios from "axios";
import { ErrorResponseDTO } from "../interfaces/ErrorResponseDTO";
import { ItemCredentialsDTO } from "../interfaces/ItemCredentialsDTO";
import { StudentDTO } from "../interfaces/StudentDTO";
import { ReportDTO } from "../interfaces/ReportDTO";

class AdminService {
	static API_URL = "/api/v1/";

	static async showReport(): Promise<ReportDTO[]> {
		const response = await axios.get<ReportDTO[]>(`${this.API_URL}report`);
		return response.data;
	}

	static async showStudents(): Promise<StudentDTO[]> {
		const response = await axios.get<StudentDTO[]>(`${this.API_URL}students`);
		return response.data;
	}

	static async showItems(): Promise<ItemCredentialsDTO[]> {
		const response = await axios.get<ItemCredentialsDTO[]>(`${this.API_URL}items`);
		return response.data;
	}

	static async evictStudent(userId: string): Promise<ErrorResponseDTO> {
		const response = await axios.delete<ErrorResponseDTO>(`${this.API_URL}students/evictStudent/${userId}`);
		return response.data;
	}

	static async addItemsToStorage(item: ItemCredentialsDTO): Promise<ErrorResponseDTO> {
		const response = await axios.post<ErrorResponseDTO>(`${this.API_URL}storage`, item);
		return response.data;
	}
}

export default AdminService;
