import { memo } from "react";
import type { FC, ReactNode } from "react";
import classes from "../../styles/Titles/TitleAppSecondary.module.css";
import resets from "../../styles/_resets.module.css";

interface Props {
    className?: string;
    text?: ReactNode;
    children?: React.ReactNode;
}

const TitleAppSecondary: FC<Props> = memo(function TitleAppSecondary(props = {}) {
	return (
		<div className={`${resets.storybrainResets} ${classes.root}`}>
			<div className={classes.rectangle}></div>
			<div className={classes.frame7}>
				<div className={classes.titleText}>PAPERKILLER</div>
			</div>
		</div>
	);
});


export default TitleAppSecondary;

