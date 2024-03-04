import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import StudentService from "../services/StudentService";
import LinenService from "../services/LinenService";
import { MyItemsDTO } from "../interfaces/MyItemsDTO";
import IconButton from "../components/Buttons/IconButton";
import ExitButton from "../components/Buttons/ExitButton";
import TitleForm from "../components/Titles/TitleForm";
import TitleAppSecondary from "../components/Titles/TitleAppSecondary";
import classes from "../styles/Pages/ChangeLinenForm.module.css";
import LinenTable from "../components/Buttons/LinenTable";
import change from "../assets/change.png";
import pass from "../assets/pass.png";

const ChangeLinen: React.FC = () => {
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

	const handleExchangeLinen = async (userID: string, itemName: string) => {
		try {
			const response = await LinenService.exchangeLinen(userID, itemName);
			console.log(response);
			await fetchItems();
		} catch (error) {
			console.error("Ошибка при обмене", error);
		}
	};

	const handlePassLinen = async (userID: string, itemName: string) => {
		try {
			const response = await LinenService.passLinen(userID, itemName);
			console.log(response);
			await fetchItems();
		} catch (error) {
			console.error("Ошибка при сдаче", error);
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
				<TitleForm>Обмен белья</TitleForm>
			</div>

			<div className={classes.title}>
				<TitleAppSecondary />
			</div>

			<div className={classes.tableAndButtonContainer}>
				<div className={classes.tables}>
					<LinenTable items={items} />
				</div>

				<div className={classes.imageElement}>
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeLinen(userID, "Bedsheet")} imageSrc={change} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeLinen(userID, "Bedspread")} imageSrc={change} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeLinen(userID, "Pillowcase")} imageSrc={change} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeLinen(userID, "Duvet")} imageSrc={change} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handleExchangeLinen(userID, "Towel")} imageSrc={change} />
				</div>
				<div className={classes.imageElement}>
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassLinen(userID, "Bedsheet")} imageSrc={pass} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassLinen(userID, "Bedspread")} imageSrc={pass} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassLinen(userID, "Pillowcase")} imageSrc={pass} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassLinen(userID, "Duvet")} imageSrc={pass} />
					<IconButton className={classes.buttonRightMargin} onClick={() => userID && handlePassLinen(userID, "Towel")} imageSrc={pass} />
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

export default ChangeLinen;
