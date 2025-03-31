export default function (props: any) {
      
    const d = [
        {firstname: 'Иван', lastname: 'Иванов', midname: 'Иванович', age:20},
        {firstname: 'Иван1', lastname: 'Иванов2', midname: 'Иванович3', age:21},
        {firstname: 'Иван2', lastname: 'Иванов2', midname: 'Иванович3', age:22},
      ]
    
    return (
        <>
          {d.map((row: Person) => <div>{row.lastname} {row.firstname} {row.midname}</div>)}
          {props.superparam}
        </>
    );
}