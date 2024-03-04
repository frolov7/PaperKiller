// services/ItemService.ts
import axios from "axios";
import { ErrorResponseDTO } from "../interfaces/ErrorResponseDTO";

class ItemService {
	static API_URL = "/api/v1/";

	static async exchangeItem(userID: string, itemName: string): Promise<ErrorResponseDTO> {
		try {
			const response = await axios.post(`${this.API_URL}exchange/${userID}/items/${itemName}`);
			return response.data;
		} catch (error) {
			console.error("Error exchanging item:", error);
			throw error;
		}
	}

	static async passItem(userID: string, itemName: string): Promise<ErrorResponseDTO> {
		try {
			const response = await axios.put(`${this.API_URL}pass/${userID}/items/${itemName}`);
			return response.data;
		} catch (error) {
			console.error("Error passing item:", error);
			throw error;
		}
	}

	static async giveItem(userID: string, itemName: string): Promise<ErrorResponseDTO> {
		try {
			const response = await axios.put(`${this.API_URL}give/${userID}/items/${itemName}`);
			return response.data;
		} catch (error) {
			console.error("Error giving item:", error);
			throw error;
		}
	}
}

export default ItemService;
