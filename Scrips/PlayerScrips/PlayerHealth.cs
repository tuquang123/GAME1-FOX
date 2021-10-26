using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public HPBar hPBar;
	public Shaker shaker;
	public GameObject FloatTextPrefab;


	public int health = 1000;

	public void start()
    {
		hPBar.SetMaxHealth(health);
		shaker = GameObject.FindGameObjectWithTag("Screen").GetComponent<Shaker>();
	}


	public void TakeDamage(int damage)
	{

		if (FloatTextPrefab && health > 0)
		{
			ShowFloatingText();
			

		}
		health -= damage;
		shaker.CamShake();
		hPBar.SetHealth(health);
		


		StartCoroutine(DamageAnimation());

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	void ShowFloatingText()
	{
		var go = Instantiate(FloatTextPrefab, transform.position, Quaternion.identity, transform);
		go.GetComponent<TextMesh>().text = health.ToString();
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}

}