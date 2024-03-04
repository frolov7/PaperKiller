import React from "react";
import classes from "../../styles/Dialog.module.css";

interface DialogProps {
    title: string;
    message: string;
    onClose: () => void;
    className?: string; // Добавляем возможность передачи className через пропсы
    classes?: {
        dialogContainer?: string;
        dialogContent?: string;
        closeDialog?: string;
        dialogCloseButton?: string;
    };
}

const Dialog: React.FC<DialogProps> = ({
	title,
	message,
	onClose,
	className,
	classes: customClasses,
}) => {
	return (
		<section className={classes.modalContainer}>
			<header>
				<h2>{title}</h2>
			</header>
			<div className="error-msg">
				<p>{message}</p>
			</div>
			<footer className="modal-close">
				<button type="button" onClick={onClose}>Закрыть</button>
			</footer>
		</section>
	);
};

export default Dialog;
