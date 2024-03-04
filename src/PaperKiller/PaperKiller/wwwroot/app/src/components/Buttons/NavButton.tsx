import React from "react";
import classes from "../../styles/Buttons/NavButton.module.css";
import resets from "../../styles/_resets.module.css";

interface ButtonProps {
    onClick: (() => void) | ((event: React.FormEvent) => Promise<void>);
    children?: React.ReactNode;
    className?: string;
}

const NavButton: React.FC<ButtonProps> = ({
	onClick,
	children,
	className,
}) => {
	return (
		<button
			onClick={onClick}
			className={`${resets.storybrainResets} ${classes.root} ${className || ""}`}>
			<div className={classes.rectangle}></div>
			<div className={classes.unnamed}>{children}</div>
		</button>
	);
};

export default NavButton;
