// services/LinenService.ts
import axios from "axios";
import { ErrorResponseDTO } from "../interfaces/ErrorResponseDTO";

class LinenService {
	static API_URL = "/api/v1/";

	static async exchangeLinen(userID: string, linenName: string): Promise<ErrorResponseDTO> {
		try {
			const response = await axios.post(`${this.API_URL}exchange/${userID}/linen/${linenName}`);
			return response.data;
		} catch (error) {
			console.error("Error exchanging linen:", error);
			throw error;
		}
	}

	static async passLinen(userID: string, linenName: string): Promise<ErrorResponseDTO> {
		try {
			const response = await axios.put(`${this.API_URL}pass/${userID}/linen/${linenName}`);
			return response.data;
		} catch (error) {
			console.error("Error passing linen:", error);
			throw error;
		}
	}

	static async giveLinen(userID: string, linenName: string): Promise<ErrorResponseDTO> {
		try {
			const response = await axios.put(`${this.API_URL}give/${userID}/linen/${linenName}`);
			return response.data;
		} catch (error) {
			console.error("Error giving linen:", error);
			throw error;
		}
	}
}

export default LinenService;
