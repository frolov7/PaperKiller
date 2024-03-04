import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ExitButton from "../components/Buttons/ExitButton";
import ItemsTable from "../components/Buttons/ItemsTable";
import LinenTable from "../components/Buttons/LinenTable";
import StudentService from "../services/StudentService";
import classes from "../styles/Pages/MyItemsForm.module.css";
import { MyItemsDTO } from "../interfaces/MyItemsDTO";
import TitleForm from "../components/Titles/TitleForm";
import TitleAppSecondary from "../components/Titles/TitleAppSecondary";

const MyItems: React.FC = () => {
	const navigate = useNavigate();
	const [userID, setUserID] = useState<string | null>(null);
	const [items, setItems] = useState<MyItemsDTO[]>([]);

	useEffect(() => {
		const userIdFromStorage = localStorage.getItem("userID");
		if (userIdFromStorage) {
			setUserID(userIdFromStorage);
		}
	}, []);

	useEffect(() => {
		fetchItems();
	}, [userID]);


	const fetchItems = async () => {
		if (userID) {
			const items = await StudentService.showMyItems(userID);
			setItems(items);
		}
	};

	const handleLogout = () => {
		navigate("/usermenu");
	};


	return (
		<div className={classes.main}>
			<div className={classes.titlepage}>
				<TitleForm>Мои вещи</TitleForm>
			</div>

			<div className={classes.title}>
				<TitleAppSecondary />
			</div>

			<div className={classes.tableAndButtonContainer}>
				<LinenTable items={items} />
				<ItemsTable items={items} />
			</div>

			<div className={classes.gridcontainer}>
				<div className={classes.buttons}>
				</div>
			</div>

			<div className={classes.exit}>
				<ExitButton onClick={handleLogout} />
			</div>
		</div>
	);
};

export default MyItems;
