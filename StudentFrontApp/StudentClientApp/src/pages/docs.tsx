import DeveloperList from "@/components/DeveloperList";
import { Input, Slider, Switch } from "antd";
import { useState } from "react";

const DocsPage = () => {

  type Person = {
    firstname: string;
    lastname: string;
    midname: string;
    age: number;
  }

  let a = 10;
  a = 12
  // Магические ковычки
  let bsa = `fsdfasdfasdf s ${a}`

  const b = 10
  const [disabled, setDisabled] = useState(false);
  const onChange = (checked: boolean) => {
    setDisabled(checked);
  };
  const c = {firstname: 'Иван', lastname: 'Иванов', midname: 'Иванович', age:20};
  const d: Person[] = [
    {firstname: 'Иван', lastname: 'Иванов', midname: 'Иванович', age:20},
    {firstname: 'Иван1', lastname: 'Иванов2', midname: 'Иванович3', age:21},
    {firstname: 'Иван2', lastname: 'Иванов2', midname: 'Иванович3', age:22},
  ]


  return (
    <div>
      <p>Ваше ФИО: {c.lastname} {c.firstname} {c.midname}</p>
      <DeveloperList superparam="Супер текст"/>
      <Input />
      <>
      <Slider defaultValue={30} disabled={disabled} />
      <Slider range defaultValue={[20, 50]} disabled={disabled} />
      Disabled: <Switch size="small" checked={disabled} onChange={onChange} />
      </>
    </div>
  );
};

export default DocsPage;
