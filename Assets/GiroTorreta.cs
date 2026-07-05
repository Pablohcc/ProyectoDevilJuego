using UnityEngine;

public class GiroTorreta : MonoBehaviour
{
    public Transform PointerPlayer;
	public float rotationSpeed=5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointerPlayer = GameObject.FindGameObjectWithTag("PlayerImpactoBala").transform;
    }

    // Update is called once per frame
    void Update()
    {
		//FaceTarget();   

		if (PointerPlayer != null)
		{
			// 1. Obtener la dirección restando las posiciones (Destino - Origen)
			Vector3 direction = PointerPlayer.position - transform.position;

			// 2. Opcional: Si NO quieres que el objeto se incline hacia arriba/abajo (solo rotar en Y)
			// direction.y = 0; 

			// 3. Verificar que la dirección no sea cero para evitar errores
			if (direction != Vector3.zero)
			{
				// 4. Calcular la rotación hacia esa dirección
				Quaternion targetRotation = Quaternion.LookRotation(direction);

				// 5. Rotar suavemente en cada frame usando Lerp o Slerp
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			}
			FaceTargetOnlyX();
		}

	}

	void FaceTarget()
	{
		Vector3 direction = (PointerPlayer.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);


	}



	void FaceTargetOnlyX()
	{
		if (PointerPlayer != null)
		{
			// 1. Calculamos la dirección hacia el jugador (Destino - Origen)
			Vector3 direction = PointerPlayer.position - transform.position;

			if (direction != Vector3.zero)
			{
				// 2. Calculamos la rotación total que se necesitaría para mirar al jugador
				Quaternion totalRotation = Quaternion.LookRotation(direction);

				// 3. Extraemos el ángulo X que se necesita para esa rotación
				float targetXAngle = totalRotation.eulerAngles.x;

				// 4. Creamos una nueva rotación usando el X calculado, 
				// pero manteniendo el Y y Z actuales del objeto
				Quaternion targetRotation = Quaternion.Euler(targetXAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);

				// 5. Aplicamos la rotación (puedes usar Slerp para que sea suave)
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
			}
		}
	}
}
