using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
	public GameObject FloatTextPrefab;


	public Animator animator;
	public int maxHealth = 500;
	int currentHealth;
	
	public HPBar hPBar;

	private Rigidbody2D rb;

	public bool isInvulnerable = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		currentHealth = maxHealth;
		hPBar.SetMaxHealth(maxHealth);
	}

	public void TakeDamage(int damage)
	{
		if (FloatTextPrefab && currentHealth > 0)
		{
			ShowFloatingText();
		}
		if (isInvulnerable)
			return;

		currentHealth -= damage;

		hPBar.SetHealth(currentHealth);

		StartCoroutine(DamageAnimation()); 


		if (currentHealth <= 200)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}
        

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		animator.SetBool("IsDead", true);
		//if die collier2d off 
		GetComponent<Collider2D>().enabled = false;
		this.enabled = false;
		Destroy(gameObject, 1.5f);
	}
	void ShowFloatingText()
    {
		var go = Instantiate(FloatTextPrefab, transform.position, Quaternion.identity ,transform);
		go.GetComponent<TextMesh>().text = currentHealth.ToString();
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