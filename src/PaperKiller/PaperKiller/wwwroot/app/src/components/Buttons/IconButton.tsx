import React from "react";
import classes from "../../styles/Buttons/Icon.module.css";
import resets from "../../styles/_resets.module.css";

interface ButtonProps {
    onClick: (() => void) | ((event: React.MouseEvent<HTMLDivElement, MouseEvent>) => void);
    children?: React.ReactNode;
    className?: string;
    classes?: {
        icon?: string;
        root?: string;
    };
    imageSrc?: string;
}

const IconButton: React.FC<ButtonProps> = ({
	onClick,
	children,
	className,
	classes: customClasses,
	imageSrc,
}) => {
	const handleClick = (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
		if (onClick) {
			onClick(event);
		}
	};

	return (
		<div
			className={`${resets.storybrainResets} ${customClasses?.root || ""} ${className || ""} ${classes.root} ${classes.buttonsContainer}`}
			role="button"
			tabIndex={0}
			onClick={handleClick}
		>
			{imageSrc ? (
				<img
					src={`${process.env.PUBLIC_URL}${imageSrc}`}
					alt=""
					className={`${customClasses?.icon || ""} ${classes.icon}`}
				/>
			) : (
				<div className={`${customClasses?.icon || ""} ${classes.icon}`}></div>
			)}
		</div>
	);
};

export default IconButton;
