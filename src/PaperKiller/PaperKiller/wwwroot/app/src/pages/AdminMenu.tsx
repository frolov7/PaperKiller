import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ExitButton from "../components/Buttons/ExitButton";
import NavButton from "../components/Buttons/NavButton";
import TitleForm from "../components/Titles/TitleForm";
import StudentTable from "../components/Buttons/StudentTable";
import StudentService from "../services/StudentService";
import classes from "../styles/Pages/AdminMenuForm.module.css";
import { StudentDTO } from "../interfaces/StudentDTO";
import TitleAppSecondary from "../components/Titles/TitleAppSecondary";

const AdminMenu: React.FC = () => {
	const navigate = useNavigate();

	const handleLogout = () => {
		navigate("/login");
	};

	return (
		<div className={classes.main}>
			<div className={classes.titlepage}>
				<TitleForm>Главное меню</TitleForm>
			</div>

			<div className={classes.title}>
				<TitleAppSecondary />
			</div>

			<div className={classes.gridcontainer}>
				<div className={classes.buttons}>
					<NavButton className={classes.button} onClick={handleLogout}>Обменять постель</NavButton>
					<NavButton className={classes.button} onClick={handleLogout}>Обменять вещи</NavButton>
				</div>

				<div className={classes.buttons}>
					<NavButton className={classes.myItemsButton} onClick={handleLogout}>Мои вещи</NavButton>
					<NavButton className={classes.button} onClick={handleLogout}>Выселиться</NavButton>
				</div>

				<div className={classes.buttons}>
					<NavButton className={classes.myItemsButton} onClick={handleLogout}>Мои вещи</NavButton>
					<NavButton className={classes.myItemsButton} onClick={handleLogout}>Мои вещи</NavButton>
					<NavButton className={classes.button} onClick={handleLogout}>Выселиться</NavButton>
				</div>
			</div>

			<div className={classes.exit}>
				<ExitButton onClick={handleLogout} />
			</div>
		</div>
	);
};

export default AdminMenu;
