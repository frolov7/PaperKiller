import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ExitButton from "../components/Buttons/ExitButton";
import ItemsTable from "../components/Buttons/ItemsTable";
import IconButton from "../components/Buttons/IconButton";
import StudentService from "../services/StudentService";
import { MyItemsDTO } from "../interfaces/MyItemsDTO";
import ItemService from "../services/ItemService";
import TitleForm from "../components/Titles/TitleForm";
import TitleAppSecondary from "../components/Titles/TitleAppSecondary";
import classes from "../styles/Pages/ChangeItemsForm.module.css";
import change from "../assets/change.png";
import pass from "../assets/pass.png";

const ChangeItems: React.FC = () => {
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

	const handleLogout = () => {
		navigate("/usermenu");
	};

	const handleExchangeItems = async (userID: string, itemName: string) => {
		try {
			const response = await ItemService.exchangeItem(userID, itemName);
			console.log(response);
			await fetchItems();
		} catch (error) {
			console.error("Ошибка при обмене предмета", error);
		}
	};

	const handlePassItems = async (userID: string, itemName: string) => {
		try {
			const response = await ItemService.passItem(userID, itemName);
			console.log(response);
			await fetchItems();
		} catch (error) {
			console.error("Ошибка при обмене предмета", error);
		}
	};

	const fetchItems = async () => {
		if (userID) {
			const items = await StudentService.showMyItems(userID);
			setItems(items);
		}
	};


	const handleBack = () => {
		navigate("/usermenu");
	};



	return (
		<div className={classes.main}>
			<div className={classes.titlepage}>
				<TitleForm>Обмен вещей</TitleForm>
			</div>

			<div className={classes.title}>
				<TitleAppSecondary />
			</div>

			<div className={classes.tableAndButtonContainer}>
				<div className={classes.tables}>
					<ItemsTable items={items} />
				</div>

				<div className={classes.imageElement}>
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeItems(userID, "Chair")} imageSrc={change} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeItems(userID, "Tables")} imageSrc={change} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeItems(userID, "Shelf")} imageSrc={change} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeItems(userID, "Wardrobe")} imageSrc={change} />
				</div>
				<div className={classes.imageElement}>
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassItems(userID, "Chair")} imageSrc={pass} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassItems(userID, "Tables")} imageSrc={pass} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassItems(userID, "Shelf")} imageSrc={pass} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassItems(userID, "Wardrobe")} imageSrc={pass} />
				</div>
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

export default ChangeItems;