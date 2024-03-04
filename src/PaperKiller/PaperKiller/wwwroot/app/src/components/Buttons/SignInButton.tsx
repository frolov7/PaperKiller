import React from "react";
import classes from "../../styles/Buttons/SignInButton.module.css";
import resets from "../../styles/_resets.module.css";

interface ButtonProps {
    onClick: (() => void) | ((event: React.FormEvent) => Promise<void>);
    children?: React.ReactNode;
    className?: string;
}

const SignInButton: React.FC<ButtonProps> = ({
	onClick,
	children,
	className,
}) => {
	return (
		<div className={`${resets.storybrainResets} ${classes.root}`} onClick={onClick}>
			<div className={`${classes.rectangle2}`}></div>
			<div className={classes.unnamed}>Войти</div>
		</div>
	);
};

export default SignInButton;