import axios from "axios";
import UserService from "../src/services/UserService";
import { AuthorizationDTO } from "../src/interfaces/AuthorizationDTO";
import { RegistrationDTO } from "../src/interfaces/RegistrationDTO";
import { StudentDTO } from "../src/interfaces/StudentDTO";

// Мокируем axios
jest.mock("axios");

describe("UserService", () => {
	const mockedPost = jest.spyOn(axios, "post").mockImplementation(() => Promise.resolve({ data: {} }));

	afterEach(() => {
		mockedPost.mockReset();
	});

	it("should register user", async () => {
		const registrationData: RegistrationDTO = {
			name: "John",
			surname: "Doe",
			phone: "+78888455555",
			studentID: "123456",
			gender: "male",
			roomNumber: "101",
			login: "student123",
			password: "password123",
			passwordRepeat: "password123"
		};
		const mockResponse =
        {
        	"errorCode": 200,
        	"errorMessage": "Успешная регистрация"
        };

		// Устанавливаем мок для axios.post
		mockedPost.mockResolvedValueOnce({ data: mockResponse });

		const response = await UserService.registerUser(registrationData);
		expect(response).toEqual(mockResponse);
		expect(mockedPost).toHaveBeenCalledWith(`${UserService.API_URL}register`, registrationData);
	});
});
