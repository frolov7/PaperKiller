import axios from "axios";
import AdminService from "../src/services/AdminService"; // Убедитесь, что путь к AdminService корректен
import { ReportDTO } from "../src/interfaces/ReportDTO";
import { StudentDTO } from "../src/interfaces/StudentDTO";
import { ItemCredentialsDTO } from "../src/interfaces/ItemCredentialsDTO";

jest.mock("axios");

describe("AdminService", () => {
	it("should fetch reports", async () => {
		// Заполнение данными ReportDTO
		const mockResponse: ReportDTO[] = [
			{
				id:   1,
				name: "John",
				surname: "Doe",
				dateChange: "2023-01-01",
				roomNumber: "101",
				itemType: "Laptop"
			},
			{
				id:   2,
				name: "Jane",
				surname: "Doe",
				dateChange: "2023-01-02",
				roomNumber: "102",
				itemType: "Desktop"
			}
		];

		(axios.get as jest.MockedFunction<typeof axios.get>).mockResolvedValueOnce({ data: mockResponse });

		const reports = await AdminService.showReport();
		expect(reports).toEqual(mockResponse);
		expect(axios.get).toHaveBeenCalledWith(`${AdminService.API_URL}report`);
	});

	it("should fetch students", async () => {
		const mockResponse: StudentDTO[] = [
			{
				id:   1,
				login: "student123",
				password: "password123",
				userType: "student",
				name: "John",
				surname: "Doe",
				phoneNumber: "123-456-7890",
				checkInDate: "2023-01-01",
				studak: "123456",
				gender: "male",
				roomNumber: "101",
				linenId:   1,
				itemsId: null,
			}
		];
		(axios.get as jest.MockedFunction<typeof axios.get>).mockResolvedValueOnce({ data: mockResponse });

		const students = await AdminService.showStudents();
		expect(students).toEqual(mockResponse);
		expect(axios.get).toHaveBeenCalledWith(`${AdminService.API_URL}students`);
	});

	it("should fetch items", async () => {
		const mockResponse: ItemCredentialsDTO[] = [
			{
				itemName: "Laptop",
				serialNumber: "1234567890"
			}
		];
		(axios.get as jest.MockedFunction<typeof axios.get>).mockResolvedValueOnce({ data: mockResponse });

		const items = await AdminService.showItems();
		expect(items).toEqual(mockResponse);
		expect(axios.get).toHaveBeenCalledWith(`${AdminService.API_URL}items`);
	});
});
