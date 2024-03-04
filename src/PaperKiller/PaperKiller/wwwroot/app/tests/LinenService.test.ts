import axios from "axios";
import LinenService from "../src/services/LinenService";
import { ErrorResponseDTO } from "../src/interfaces/ErrorResponseDTO";

jest.mock("axios");

describe("LinenService", () => {
	it("should exchange an linen", async () => {
		const userID = "2";
		const itemName = "Duvet";
		const mockResponse: ErrorResponseDTO = { 
			"errorCode": 200,
			"errorMessage": "Duvet успешно обменян(а)"
		};
		(axios.post as jest.MockedFunction<typeof axios.post>).mockResolvedValueOnce({ data: mockResponse });

		const errorResponse = await LinenService.exchangeLinen(userID, itemName);
		expect(errorResponse).toEqual(mockResponse);
		expect(axios.post).toHaveBeenCalledWith(`${LinenService.API_URL}exchange/${userID}/linen/${itemName}`);
	});
});
