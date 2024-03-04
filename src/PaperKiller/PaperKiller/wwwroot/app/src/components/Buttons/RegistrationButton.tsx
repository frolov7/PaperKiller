import React from "react";
import classes from "../../styles/Buttons/RegistrationButton.module.css";
import resets from "../../styles/_resets.module.css";

interface ButtonProps {
    onClick: () => void;
    className?: string;
    children?: React.ReactNode;
}

const RegistrationButton: React.FC<ButtonProps> = ({
	onClick,
	children,
	className,

}) => {
	return (
		<div className={`${resets.storybrainResets} ${classes.root}`} onClick={onClick}>
			<div className={`${classes.rectangle3}`}></div>
			<div className={`${classes.frame4}`}>
				<div className={classes.unnamed}>Зарегистрироваться</div>
			</div>
		</div>
	);
};


export default RegistrationButton;