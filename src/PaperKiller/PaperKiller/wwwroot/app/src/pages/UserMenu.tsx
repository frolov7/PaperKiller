import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ExitButton from "../components/Buttons/ExitButton";
import NavButton from "../components/Buttons/NavButton";
import TitleForm from "../components/Titles/TitleForm";
import StudentTable from "../components/Buttons/StudentTable";
import StudentService from "../services/StudentService";
import classes from "../styles/Pages/UserMenuForm.module.css";
import { StudentDTO } from "../interfaces/StudentDTO";
import TitleAppSecondary from "../components/Titles/TitleAppSecondary";

const UserMenu: React.FC = () => {
	const navigate = useNavigate();
	const [userID, setUserID] = useState<string | null>(null);
	const [student, setStudent] = useState<StudentDTO | null>(null);

	useEffect(() => {
		const storedUserId = localStorage.getItem("userID");
		if (storedUserId) {
			setUserID(storedUserId);
		}
	}, []);

	useEffect(() => {
		if (userID) {
			fetchStudentData();
		}
	}, [userID]);

	const fetchStudentData = async () => {
		if (!userID) {
			console.error("User ID not found");
			return;
		}
		try {
			const studentData = await StudentService.getStudentData(userID);
			setStudent(studentData);
		} catch (error) {
			console.error("Ошибка при получении данных студента:", error);
		}
	};

	const handleLogout = () => {
		navigate("/login");
	};

	const handleChangeData = () => {
		navigate("/changedata");
	};

	const handleChangeLinen = () => {
		navigate("/changelinen");
	};

	const handleChangeItems = () => {
		navigate("/changeitems");
	};

	const handleMoveOut = async () => {
		if (!userID) {
			console.error("Student data not found");
			return;
		}

		try {
			await StudentService.moveOut(userID);
			fetchStudentData();
			navigate("/usermenu");
		} catch (error) {
			console.error("Ошибка при выселении студента:", error);
		}
	};

	const handleMyItems = () => {
		navigate("/myitems");
	};

	return (
		<div className={classes.main}>
			<div className={classes.titlepage}>
				<TitleForm>Главное меню</TitleForm>
			</div>

			<div className={classes.title}>
				<TitleAppSecondary />
			</div>

			<div className={classes.tableAndButtonContainer}>
				{student && <StudentTable student={student} />}
				<NavButton onClick={handleChangeData}>Изменить данные</NavButton>
			</div>

			<div className={classes.gridcontainer}>
				<div className={classes.buttons}>
					<NavButton className={classes.button} onClick={handleChangeLinen}>Обменять постель</NavButton>
					<NavButton className={classes.button} onClick={handleChangeItems}>Обменять вещи</NavButton>
				</div>
				<div className={classes.buttons}>
					<NavButton className={classes.myItemsButton} onClick={handleMyItems}>Мои вещи</NavButton>
					<NavButton className={classes.button} onClick={handleMoveOut}>Выселиться</NavButton>
				</div>
			</div>

			<div className={classes.exit}>
				<ExitButton onClick={handleLogout} />
			</div>
		</div>
	);
};

export default UserMenu;
