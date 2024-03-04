import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { StudentChangeDataDTO } from "../interfaces/StudentChangeDataDTO";
import { StudentDTO } from "../interfaces/StudentDTO";
import StudentService from "../services/StudentService";
import ExitButton from "../components/Buttons/ExitButton";
import FieldForm from "../components/Buttons/FieldForm";
import UtilButton from "../components/Buttons/UtilButton";
import name_img from "../assets/user_icon.png";
import surname_img from "../assets/user_icon2.png";
import phone_img from "../assets/phone_icon.png";
import classes from "../styles/Pages/ChangeDataForm.module.css";
import TitleAppSecondary from "../components/Titles/TitleAppSecondary";
import TitleForm from "../components/Titles/TitleForm";

const ChangeDataButton: React.FC = () => {
	const navigate = useNavigate();

	const [newName, setName] = useState("");
	const [newSurname, setSurname] = useState("");
	const [newPhone, setPhone] = useState("");
	const [userId, setUserId] = useState<string | null>(null);

	useEffect(() => {
		const storedUserId = localStorage.getItem("userID");
		if (storedUserId) {
			setUserId(storedUserId);
		}
	}, []);

	const handleLogout = () => {
		navigate("/usermenu");
	};

	const handleSubmit = async (event: React.FormEvent) => {
		event.preventDefault();
		const studentChangeData: StudentChangeDataDTO = {
			newName,
			newSurname,
			newPhone
		};

		if (!userId) {
			console.error("Student data not found");
			return;
		}

		try {
			const response = await StudentService.updateStudentData(userId, studentChangeData);
			if (response.status === 200) {
				console.log("Student data successfully updated");
			} else {
				console.error("Error updating student data");
			}
			navigate("/usermenu");
		} catch (error) {
			console.error("Change data failed:", error);
		}
	};

	return (
		<div className={classes.main}>
			<div className={classes.inputAndButtonsContainer}>
				<div className={classes.gridcontainer}>
					<FieldForm
						type="text"
						value={newName}
						onChange={e => setName(e.target.value)}
						placeholder="Name"
						iconSrc={name_img}
					/>
					<FieldForm
						type="tel"
						value={newPhone}
						onChange={e => setPhone(e.target.value)}
						placeholder="Phone"
						iconSrc={phone_img}
					/>
				</div>
				<div className={classes.gridcontainer}>
					<FieldForm
						type="text"
						value={newSurname}
						onChange={e => setSurname(e.target.value)}
						placeholder="Surname"
						iconSrc={surname_img}
					/>
					<UtilButton onClick={handleSubmit}>Принять</UtilButton>
				</div>
			</div>
			<div className={classes.titlepage}>
				<TitleForm>Сменить данные</TitleForm>
			</div>
			<div className={classes.title}>
				<TitleAppSecondary />
			</div>
			<div className={classes.exit}>
				<ExitButton onClick={handleLogout} />
			</div>
		</div>
	);

};

export default ChangeDataButton;
