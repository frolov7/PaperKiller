import React, { useState, useEffect } from "react";
import UserService from "../services/UserService";
import { RegistrationDTO } from "../interfaces/RegistrationDTO";
import ExitButton from "../components/Buttons/ExitButton";
import UtilButton from "../components/Buttons/UtilButton";
import FieldForm from "../components/Buttons/FieldForm";
import { useNavigate } from "react-router-dom";
import classes from "../styles/Pages/RegistrationForm.module.css";
import TitleAppSecondary from "../components/Titles/TitleAppSecondary";
import TitleForm from "../components/Titles/TitleForm";

import name_img from "../assets/user_icon.png";
import surname_img from "../assets/user_icon2.png";
import login_img from "../assets/login_icon.png";
import phone_img from "../assets/phone_icon.png";
import studak_img from "../assets/studak1.png";
import password_img from "../assets/secure_icon.png";
import passrepeat_img from "../assets/secure_icon2.png";
const RegisterButton: React.FC = () => {
	const navigate = useNavigate();

	const [name, setName] = useState("");
	const [surname, setSurname] = useState("");
	const [phone, setPhone] = useState("");
	const [studentID, setStudentID] = useState("");
	const [gender, setGender] = useState("");
	const [roomNumber, setRoomNumber] = useState("");
	const [login, setLogin] = useState("");
	const [password, setPassword] = useState("");
	const [passwordRepeat, setPasswordRepeat] = useState("");

	const handleLogout = () => {
		navigate("/login");
	};

	const handleGenderSelection = (genderValue: string) => {
		setGender(genderValue);
	};

	const handleSubmit = async (event: React.FormEvent) => {
		event.preventDefault();

		const registrationData: RegistrationDTO = {
			name,
			surname,
			phone,
			studentID,
			gender,
			roomNumber,
			login,
			password,
			passwordRepeat,
		};

		try {
			await UserService.registerUser(registrationData);
			console.log("User registered successfully");
			navigate("/login");
		} catch (error) {
			console.error("Error registering user:", error);
		}
	};

	return (
		<div className={classes.main}>
			<div className={classes.inputAndButtonsContainer}>
				<div className={classes.inputField}>
					<FieldForm
						type="text"
						value={name}
						onChange={e => setName(e.target.value)}
						placeholder="Имя"
						iconSrc={name_img}
					/>
					<FieldForm
						type="text"
						value={login}
						onChange={e => setLogin(e.target.value)}
						placeholder="Логин"
						iconSrc={login_img}
					/>
					<FieldForm
						type="password"
						value={password}
						onChange={e => setPassword(e.target.value)}
						placeholder="Пароль"
						iconSrc={password_img}
					/>
					<FieldForm
						type="password"
						value={passwordRepeat}
						onChange={e => setPasswordRepeat(e.target.value)}
						placeholder="Повторите пароль"
						iconSrc={passrepeat_img}
					/>
				</div>
				<div className={classes.inputField}>
					<FieldForm
						type="text"
						value={surname}
						onChange={e => setSurname(e.target.value)}
						placeholder="Фамилия"
						iconSrc={surname_img}
					/>
					<FieldForm
						type="tel"
						value={phone}
						onChange={e => setPhone(e.target.value)}
						placeholder="Номер телефона"
						iconSrc={phone_img}
					/>
					<FieldForm
						type="text"
						value={studentID}
						onChange={e => setStudentID(e.target.value)}
						placeholder="Студенческий номер"
						iconSrc={studak_img}
					/>
					<UtilButton onClick={handleSubmit}>Принять</UtilButton>
				</div>
			</div>
			<div className={classes.titlepage}>
				<TitleForm>Регистрация</TitleForm>
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

export default RegisterButton;
