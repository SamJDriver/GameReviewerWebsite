import { MdClear } from "react-icons/md";
import { BsCalendarEvent } from "react-icons/bs";
import { Calendar } from "react-date-range";
import { useState } from "react";
import classnames from 'classnames';

interface IProps {
    minDate: Date;
    maxDate: Date;
    placeholder?: string;
    className? : string;
    onChange: (date: Date | null) => void;
}

export const CalenderDatePicker = (props: IProps) => {
    const [openFlag, setOpenFlag] = useState<boolean>(false);
    const [selectedDate, setSelectedDate] = useState<Date | null>(null);
    const [inputValue, setInputValue] = useState<string>('');

    // Close the date picker when the user clicks outside of it
    document.body.addEventListener('click', function(event: MouseEvent) {
        if (!(event.target as Element).closest('.game-search--date-picker')) {
          const calendarElement = document.getElementById('calenderDiv') as HTMLElement | null;
          const clickedElementId = (event.target as Element).id;
          if (calendarElement && clickedElementId !== 'openCalendarDiv' && clickedElementId !== 'calendarDiv' && clickedElementId !=='calendarIcon') {
            setOpenFlag(false);
          } 
        }
    });

    const handleInputTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(event.target.value);
        const date = new Date(event.target.value);
        if (!isNaN(date.getTime())) {
            setSelectedDate(date);
            props.onChange(date);
        }
    };
    
    const classes = classnames('input-group', props.className);

    return (
        <>
            <div className={classes}>
                <div onClick={() => setOpenFlag(!openFlag)} id="openCalendarDiv" className="input-group-prepend input-group-text game-search--item-background-color">
                    <BsCalendarEvent id="calendarIcon"/>
                </div>
                <input 
                    type="text"
                    placeholder={props.placeholder}
                    value={inputValue} 
                    onChange={(event) => handleInputTextChange(event)}
                    className="form-control game-search--item-background-color"
                    aria-label={"search " + props.placeholder} 
                />
                <div id="calenderDiv" className="input-group-text" style={{backgroundColor: "rgb(26, 28, 30)", borderStyle: "none"}}>
                    <Calendar
                        minDate={props.minDate} 
                        maxDate={props.maxDate} 
                        className={openFlag ? "game-search--date-picker" : "game-search--date-picker-hide"}
                        onChange={ (event) => { setSelectedDate(event); setInputValue(event.toLocaleDateString()); setOpenFlag(false); props.onChange(event) } }
                    /> 
                </div>
                <div onClick={() => { setSelectedDate(null); setInputValue(''); props.onChange(null); } } className="input-group-append input-group-text game-search--item-background-color">
                    <MdClear/>
                </div>
            </div>
        </>
    );
}