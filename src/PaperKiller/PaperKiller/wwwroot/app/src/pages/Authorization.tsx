import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import UserService from "../services/UserService";
import { AuthorizationDTO } from "../interfaces/AuthorizationDTO";
import SignInButton from "../components/Buttons/SignInButton";
import RegButton from "../components/Buttons/RegistrationButton";
import FieldForm from "../components/Buttons/FieldForm";
import TitleAppMain from "../components/Titles/TitleAppMain";
import classes from "../styles/Pages/AuthorizationForm.module.css";
import login_img from "../assets/user_icon.png";
import password_img from "../assets/secure_icon.png";

const LoginPage: React.FC = () => {
	const navigate = useNavigate();

	const handleRegister = () => {
		navigate("/registration");
	};

	const [login, setLogin] = useState("");
	const [password, setPassword] = useState("");
	const [errorMessage, setErrorMessage] = useState("");


	const handleLogin = async (event: React.FormEvent) => {
		event.preventDefault();

		const authorizationData: AuthorizationDTO = {
			login,
			password,
		};

		try {
			const authResult = await UserService.authenticateUser(authorizationData);
			if (authResult === "Commandant") {
				navigate("/adminmenu");
			} else {
				navigate("/usermenu");
			}
		} catch (error) {
			console.error("Login failed:", error);
			setErrorMessage("Неверный  логин или пароль. Пожалуйста, попробуйте еще раз.");
		}
	};

	return (
		<div className={classes.main}>
			<div className={classes.titlepage}>
				<TitleAppMain />
			</div>

			<div>
				<form onSubmit={handleLogin} className={classes.form}>
					<div className={classes.inputAndButtonsContainer}>
						<div className={classes.inputField}>
							<label>
								<FieldForm
									type="text"
									value={login}
									onChange={(e: React.ChangeEvent<HTMLInputElement>) => setLogin(e.target.value)}
									placeholder="Логин"
									iconSrc={login_img}
								/>
							</label>
						</div>

						<div className={classes.inputField}>
							<label>
								<FieldForm
									type="password"
									value={password}
									onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPassword(e.target.value)}
									placeholder="Пароль"
									iconSrc={password_img}
								/>
							</label>
						</div>

						<div>
							<SignInButton onClick={handleLogin} className={classes.button}>Войти</SignInButton>
							<RegButton onClick={handleRegister} className={classes.button}>Регистрация</RegButton>
						</div>
					</div>
				</form>
			</div>
		</div>
	);
};

export default LoginPage;
