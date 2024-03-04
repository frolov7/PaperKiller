import axios from "axios";
import ItemService from "../src/services/ItemService";
import { ErrorResponseDTO } from "../src/interfaces/ErrorResponseDTO";

jest.mock("axios");

describe("ItemService", () => {
	it("should exchange an item", async () => {
		const userID = "2";
		const itemName = "Chair";
		const mockResponse: ErrorResponseDTO = { 
			"errorCode": 200,
			"errorMessage": "Chair успешно обменян(а)"
		};
		(axios.post as jest.MockedFunction<typeof axios.post>).mockResolvedValueOnce({ data: mockResponse });

		const errorResponse = await ItemService.exchangeItem(userID, itemName);
		expect(errorResponse).toEqual(mockResponse);
		expect(axios.post).toHaveBeenCalledWith(`${ItemService.API_URL}exchange/${userID}/items/${itemName}`);
	});
});
